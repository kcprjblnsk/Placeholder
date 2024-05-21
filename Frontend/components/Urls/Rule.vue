<template>
    <v-card class="mb-4" :border="true" elevation="0">
        <v-card-text>
            <v-row>
                <v-col cols="12" sm="6">
                    <v-select :rules="[ruleRequired]" variant="outlined" label="Właściwość"
                        :modelValue="props.modelValue.property"
                        @update:modelValue="updateModelValue('property', $event)" :items="properties" item-title="name"
                        item-value="value"></v-select>
                </v-col>
                <v-col cols="12" sm="6">
                    <v-select :rules="[ruleRequired]" variant="outlined" label="Operator"
                        :modelValue="props.modelValue.operator"
                        @update:modelValue="updateModelValue('operator', $event)" :items="operators" item-title="name"
                        item-value="value"></v-select>
                </v-col>
                <v-col cols="12">
                    <v-text-field :rules="[ruleRequired, ruleMaxLen(1000)]" variant="outlined" label="Wartość"
                        :modelValue="props.modelValue.value"
                        @update:modelValue="updateModelValue('value', $event)"></v-text-field>
                </v-col>
            </v-row>
        </v-card-text>
        <v-card-actions v-if="$slots.actions">
            <slot name="actions"></slot>
        </v-card-actions>
    </v-card>
</template>

<script setup>
const { ruleRequired, ruleMaxLen } = useFormValidationRules();

const props = defineProps({
    modelValue: {
        type: Object
    },
});

const emit = defineEmits(['update:modelValue'])

const updateModelValue = (key, value) => {
    emit('update:modelValue', { ...props.modelValue, [key]: value });
}

const operators = [{
    value: 'Equal',
    name: 'Równa się'
}, {
    value: 'NotEqual',
    name: 'Jest różne niż'
}, {
    value: 'GreaterThan',
    name: 'Większe niż'
}, {
    value: 'LessThan',
    name: 'Mniejsze niż'
}, {
    value: 'Contains',
    name: 'Zawiera'
}];


const properties = [{
    value: 'StatusCode',
    name: 'Kod odpowiedzi'
}, {
    value: 'Content',
    name: 'Zawartość odpowiedzi'
}, {
    value: 'ResponseTime',
    name: 'Czas odpowiedzi'
}]

</script>