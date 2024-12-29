<template>
  <v-app>
    <v-app-bar color="primary">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-app-bar-title class="d-flex align-center">
        <v-icon icon="mdi-solar-power" class="mr-2"></v-icon>
        Lumos
        <span class="text-subtitle-2 ml-2 font-weight-light"
          >Güneş Enerjisi Yönetim Platformu</span
        >
      </v-app-bar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list>
        <v-list-item
          to="/"
          prepend-icon="mdi-view-dashboard"
          title="Gösterge Paneli"
        ></v-list-item>
        <v-list-item
          to="/stations"
          prepend-icon="mdi-solar-power"
          title="Santraller"
        ></v-list-item>
        <v-list-item
          to="/reports"
          prepend-icon="mdi-chart-box"
          title="Raporlar"
        ></v-list-item>
        <v-list-item
          to="/alerts"
          prepend-icon="mdi-alert"
          title="Alarmlar"
        ></v-list-item>
        <v-list-item
          to="/settings"
          prepend-icon="mdi-cog"
          title="Ayarlar"
        ></v-list-item>
        <v-list-item
          to="/about"
          prepend-icon="mdi-information"
          title="Hakkında"
        ></v-list-item>
      </v-list>

      <template v-slot:append>
        <div class="pa-4">
          <v-btn block color="primary">
            <v-icon start icon="mdi-power"></v-icon>
            Oturumu Kapat
          </v-btn>
        </div>
      </template>
    </v-navigation-drawer>

    <v-main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-main>

    <v-footer app class="d-flex justify-space-between px-4">
      <span>Lumos - Güneş Enerjisinin Geleceği</span>
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script setup>
import { ref } from "vue";

const drawer = ref(false);
</script>

<style>
html {
  overflow-y: auto;
}

::-webkit-scrollbar {
  width: 0;
  background: transparent;
}

.v-application {
  font-family: "Roboto", sans-serif;
}

.v-main {
  --v-layout-css: none !important;
}
</style>
