import { defineStore } from 'pinia'

export const useRuleSettingsStore = defineStore({
    id: 'rule-settings-store',
    state: () => {
        return {
            loading: false,
            data: {},
        }
    },
    actions: {
        async loadData() {
            const globalMessageStore = useGlobalMessageStore();

            this.loading = true;
            const { data, error } = await useWebApiFetch('/ResultRule/Settings');
            this.loading = false;

            if (data.value) {
                this.data = data.value;
            }         
            else if (error.value) {
                globalMessageStore.showErrorMessage('Wystąpił błąd podzcas pobierania danych');
            }
        },

        async getPropertiesCompareOperators() {
            if (!this.data.propertiesCompareOperators) {
                await this.loadData();
            } 

            return this.data.propertiesCompareOperators
        }
    }
})