<template>
    <v-app>
        <v-app-bar color="brand">
            <v-app-bar-nav-icon v-if="mobile" @click="drawer = !drawer"></v-app-bar-nav-icon>

            <v-app-bar-title>Application</v-app-bar-title>
            <v-spacer></v-spacer>
            <VBtn icon="mdi-theme-light-dark" title="Przełącz motyw" @click="toogleTheme"></VBtn>

        </v-app-bar>
        <v-navigation-drawer :order="mobile ? -1 : 0" v-model="drawer" v-if="userStore.$state.isLoggedIn === true">
            <v-list-item lines="two">
                <template v-slot:prepend>
                    <v-avatar color="brand" v-if="userStore.$state.userData?.email">
                        {{ userStore.$state.userData.email[0].toUpperCase() }}
                    </v-avatar>
                </template>
                <VListItemTitle v-if="accountStore.$state.accountData?.name">{{ accountStore.$state.accountData.name }}
                </VListItemTitle>
                <VListItemSubtitle v-if="userStore.$state.userData?.email">{{ userStore.$state.userData.email }}
                </VListItemSubtitle>
            </v-list-item>
            <VDivider></VDivider>



            <VList>
                <VListItem v-for="item in menuItems" :key="item.name" :title="item.name" :prepend-icon="item.icon"
                    :to="item.url">
                </VListItem>
            </VList>
        </v-navigation-drawer>



        <v-main>
            <div class="pa-4">
                <NuxtPage v-if="userStore.$state.isLoggedIn === true" />
            </div>

        </v-main>
        <LoginUI></LoginUI>
    </v-app>
</template>

<script setup>
import { useDisplay } from 'vuetify'
import { useTheme } from 'vuetify';
import { useStorage } from '@vueuse/core';


const theme = useTheme();
const { mobile } = useDisplay();
const drawer = ref(null);
const currentTheme = useStorage('currentTheme', 'light');
const userStore = useUserStore();
const accountStore = useAccountStore();


const menuItems = [
    {
        name: 'Strona główna',
        icon: 'mdi-home',
        url: '/',
    },
    {
        name: 'Monitorowanie strony',
        icon: 'mdi-web-check',
        url: '/urls',
    }
];

function toogleTheme() {
    let newTheme = theme.global.current.value.dark ? 'light' : 'dark';
    theme.global.name.value = newTheme;
    currentTheme.value = newTheme;

}
onMounted(() => {
    theme.global.name.value = currentTheme.value;
    userStore.loadLoggedInUser();
});


</script>