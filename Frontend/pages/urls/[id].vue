<template>
    <VCard>
        <v-toolbar color="transparent">
            <template v-slot:prepend>
                <v-btn icon="mdi-chevron-left" to="/urls"></v-btn>
            </template>

            <v-toolbar-title>
                {{ isAdd ? 'Dodaj stronę do monitorowania' : 'Edycja monitorowanej strony' }}
            </v-toolbar-title>
        </v-toolbar>

        <VSkeletonLoader v-if="loading" type="paragraph, actions"></VSkeletonLoader>
        <VForm v-else @submit.prevent="submit" :disabled="saving">
            <VCardText>
                <VTextField :rules="[ruleRequired, ruleUrl, ruleMaxLen(400)]" v-model="viewModel.url" variant="outlined"
                    label="Adres url">
                </VTextField>
                <UrlsRuleSet class="mt-2" v-model="viewModel.ruleSet" :disabled="saving"></UrlsRuleSet>
            </VCardText>

            <VCardText class="text-right">
                <VBtn prepend-icon="mdi-content-save" variant="flat" color="primary" type="submit" :loading="saving"
                    :disabled="loading">Zapisz
                </VBtn>

            </VCardText>
        </VForm>
    </VCard>
</template>

<script setup>
const route = useRoute();
const { getErrorMessage } = useWebApiResponseParser();
const globalMessageStore = useGlobalMessageStore();
const router = useRouter();
const { ruleRequired, ruleUrl, ruleMaxLen } = useFormValidationRules();

const saving = ref(false);
const loading = ref(false);

const isAdd = computed(() => {
    return route.params.id === 'add';
});

const viewModel = ref({
    url: '',
    ruleSet: {
        operator: 'Or',
        rules: []
    },
});

const submit = async (ev) => {
    const { valid } = await ev;
    if (valid) {
        save();
    }
}
const save = () => {
    saving.value = true;

    useWebApiFetch(`/MonitoredUrl/CreateOrUpdate`, {
        method: 'POST',
        body: { ...viewModel.value, id: isAdd.value ? undefined : route.params.id },
        watch: false,
        onResponseError: ({ response }) => {
            let message = getErrorMessage(response, {});
            globalMessageStore.showErrorMessage(message);
        }
    })
        .then((response) => {
            if (response.data.value) {
                globalMessageStore.showSuccessMessage('Zapisano zmiany.');
                router.push({ path: '/urls' });
            }
        })
        .finally(() => {
            saving.value = false;
        });
}

const loadData = () => {
    loading.value = true;

    useWebApiFetch('/MonitoredUrl/Get', {
        query: { id: route.params.id },
    }).then(({ data, error }) => {
        if (data.value) {
            viewModel.value = data.value;
        } else if (error.value) {
            globalMessageStore.showErrorMessage("Błąd pobierania danych");
        }
    }).finally(() => {
        loading.value = false;
    });
};

onMounted(() => {
    if (!isAdd.value) {
        loadData();
    }
});
</script>