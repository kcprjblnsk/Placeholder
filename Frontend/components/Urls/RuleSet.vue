<template>
    <div>
        <div class="mb-5">Lista reguł</div>
        <v-select v-if="props.modelValue.rules.length > 0" label="Operator pomiędzy regułami" variant="outlined"
            :modelValue="props.modelValue.operator" @update:modelValue="updateModelValue('operator', $event)"
            :items="operators" item-title="name" item-value="value"></v-select>

        <div class="mb-6">
            <UrlsRule v-for="(rule, ix) in props.modelValue.rules" :key="ix" :modelValue="props.modelValue.rules[ix]"
                @update:modelValue="updateRule(ix, $event)">
                <template #actions>
                    <v-spacer></v-spacer>
                    <v-btn icon="mdi-delete" title="Usuń regułę" @click="deleteRule(ix)" :disabled="disabled"></v-btn>
                </template>
            </UrlsRule>
            <v-btn variant="tonal" @click="addRule" :disabled="disabled">Dodaj regułę</v-btn>
        </div>
    </div>
</template>

<script setup>

const props = defineProps({
    modelValue: {
        type: Object,
    },
    disabled: {
        type: Boolean,
        default: false,
    }
});

const emit = defineEmits(['update:modelValue'])

const operators = [{
    name: 'Lub',
    value: 'Or'
}, {
    name: 'Oraz',
    value: 'And'
}];

const updateModelValue = (key, value) => {
    emit('update:modelValue', { ...props.modelValue, [key]: value });
}

const updateRule = (ix, value) => {
    const rules = [...props.modelValue.rules];
    rules[ix] = value;

    updateModelValue('rules', rules);
}

const addRule = () => {
    const newRule = {
        property: 'StatusCode',
        value: '200',
        operator: 'Equal'
    };

    updateModelValue('rules', [...props.modelValue.rules, newRule]);
}

const deleteRule = (ix) => {
    const newRules = props.modelValue.rules.filter((e, i) => i !== ix);
    updateModelValue('rules', newRules);
}
</script>