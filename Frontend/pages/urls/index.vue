<template>
    <VCard>
        <v-toolbar color="transparent">
            <v-toolbar-title>
                Monitorowane strony
            </v-toolbar-title>

            <template v-slot:append>
                <div class="ml-2">
                    <v-btn color="primary" variant="flat" prepend-icon="mdi-plus" to="/urls/add">Dodaj</v-btn>
                </div>
            </template>
        </v-toolbar>

        <v-data-table :loading="loading" :items="items" :headers="headers"
            items-per-page-text="Liczba wierszy na stronę" :items-per-page-options="[10, 20, 50]"
            page-text="{0}-{1} z {2}" no-data-text="Nie dodano żadnego adresu do monitorowania"
            loading-text="Wczytywanie">

            <template v-slot:item.url="{ value }">
                <a :href="value" target="_blank">{{ value }}</a>
            </template>

            <template v-slot:item.createDate="{ value }">
                {{ dayjs(value).format('DD.MM.YYYY HH:mm') }}
            </template>

            <template v-slot:item.action="{ item }">
                <div class="text-no-wrap">
                    <VBtn icon="mdi-delete" title="Usuń" variant="flat" :loading="item.deleting"
                        @click="deleteUrl(item)"></VBtn>
                    <VBtn icon="mdi-pencil" title="Edytuj" variant="flat" :to="`/urls/${item.id}`"></VBtn>
                </div>
            </template>
        </v-data-table>
        <ConfirmDialog ref="confirmDialog" />
    </VCard>

</template>

<script setup>
const confirmDialog = ref(null);
const globalMessageStore = useGlobalMessageStore();
const { getErrorMessage } = useWebApiResponseParser();
const dayjs = useDayjs();

const loading = ref(false);
const items = ref([]);

const headers = ref([
    { title: 'Status', value: 'recentResults' },
    { title: 'Url', value: 'url' },
    { title: 'Data utworzenia', value: 'createDate' },
    { title: '', value: 'action', align: 'end' },
]);

const loadData = () => {
    loading.value = true;

    useWebApiFetch('/MonitoredUrl/List')
        .then(({ data, error }) => {
            if (data.value) {
                items.value = data.value.urls;
            } else if (error.value) {
                items.value = [];
            }
        })
        .finally(() => {
            loading.value = false;
        });
};

const deleteUrl = (item) => {
    confirmDialog.value.show({
        title: 'Potwierdź usunięcie',
        text: 'Czy na pewno chcesz usunąć adres z monitorowania?',
        confirmBtnText: 'Usuń',
        confirmBtnColor: 'error'
    }).then((confirm) => {
        if (confirm) {
            item.deleting = true;
            useWebApiFetch('/MonitoredUrl/Delete', {
                method: 'POST',
                body: { id: item.id },
                watch: false,
                onResponseError: ({ response }) => {
                    let message = getErrorMessage(response, {});
                    globalMessageStore.showErrorMessage(message);
                }
            })
                .then((response) => {
                    if (response.data.value) {
                        globalMessageStore.showSuccessMessage('Url został usunięty.');
                        let indexToDel = items.value.findIndex(i => i.id === item.id);
                        if (indexToDel > -1) {
                            items.value.splice(indexToDel, 1);
                        }
                    }
                })
                .finally(() => {
                    item.deleting = false;
                });
        }
    })
}

onMounted(() => {
    loadData();
});

</script>
