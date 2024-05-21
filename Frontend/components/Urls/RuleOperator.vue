<template>
    <v-select :rules="[ruleRequired]" variant="outlined" label="Operator" v-model="viewModel" :items="filteredOperators"
        item-title="name" item-value="value"></v-select>
</template>


<script setup>
const { ruleRequired } = useFormValidationRules();
const ruleSettingsStore = useRuleSettingsStore();

const props = defineProps({
    modelValue: {
        type: String,
    },
    ruleProperty: {
        type: String,
        required: true,
    }
});
const emit = defineEmits(['update:modelValue'])

const viewModel = computed({
    get() {
        return props.modelValue
    },
    set(value) {
        emit('update:modelValue', value)
    }
})

const propertiesOperators = ref({});

const filteredOperators = computed(() => {
    if (propertiesOperators.value) {
        let currentPropertyOperators = propertiesOperators.value[props.ruleProperty];
        if (currentPropertyOperators) {
            return operators.filter(o => currentPropertyOperators.includes(o.value));
        }
    }

    return [];
});



watch(() => props.ruleProperty, () => {
    if (filteredOperators.value.length > 0) {
        viewModel.value = filteredOperators.value[0].value;
    } else {
        viewModel.value = '';
    }
});

const loadRuleSettings = async () => {
    propertiesOperators.value = await ruleSettingsStore.getPropertiesCompareOperators();
}

onMounted(() => {
    loadRuleSettings();
});

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

</script>