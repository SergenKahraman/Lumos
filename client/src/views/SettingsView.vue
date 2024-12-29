<template>
  <div class="settings-container">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="text-h5">Ayarlar</div>
        <div class="text-subtitle-2 text-medium-emphasis">
          Sistem ve kullanıcı tercihlerinizi özelleştirin
        </div>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" md="3">
        <v-card>
          <v-list>
            <v-list-item
              v-for="(item, i) in settingMenus"
              :key="i"
              :value="item"
              :title="item.title"
              :prepend-icon="item.icon"
              :active="activeMenu === item.value"
              @click="activeMenu = item.value"
            ></v-list-item>
          </v-list>
        </v-card>
      </v-col>

      <v-col cols="12" md="9">
        <v-card>
          <v-card-title>{{ getCurrentMenuTitle }}</v-card-title>
          <v-card-text>
            <!-- Genel Ayarlar -->
            <div v-if="activeMenu === 'general'">
              <v-row>
                <v-col cols="12" md="6">
                  <v-select
                    v-model="settings.language"
                    :items="languages"
                    label="Dil"
                  ></v-select>
                </v-col>
                <v-col cols="12" md="6">
                  <v-select
                    v-model="settings.timezone"
                    :items="timezones"
                    label="Saat Dilimi"
                  ></v-select>
                </v-col>
                <v-col cols="12">
                  <v-switch
                    v-model="settings.notifications"
                    label="Bildirimler"
                  ></v-switch>
                </v-col>
              </v-row>
            </div>

            <!-- Alarm Ayarları -->
            <div v-if="activeMenu === 'alerts'">
              <v-row>
                <v-col cols="12">
                  <v-switch
                    v-model="settings.emailAlerts"
                    label="E-posta Bildirimleri"
                  ></v-switch>
                </v-col>
                <v-col cols="12">
                  <v-switch
                    v-model="settings.smsAlerts"
                    label="SMS Bildirimleri"
                  ></v-switch>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="settings.alertEmail"
                    label="Bildirim E-postası"
                    type="email"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-text-field
                    v-model="settings.alertPhone"
                    label="Bildirim Telefonu"
                    type="tel"
                  ></v-text-field>
                </v-col>
              </v-row>
            </div>

            <!-- Kullanıcı Ayarları -->
            <div v-if="activeMenu === 'user'">
              <v-row>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="settings.fullName"
                    label="Ad Soyad"
                  ></v-text-field>
                </v-col>
                <v-col cols="12" md="6">
                  <v-text-field
                    v-model="settings.email"
                    label="E-posta"
                    type="email"
                  ></v-text-field>
                </v-col>
                <v-col cols="12">
                  <v-btn color="primary" block>
                    Şifre Değiştir
                    <v-icon end icon="mdi-lock"></v-icon>
                  </v-btn>
                </v-col>
              </v-row>
            </div>

            <!-- Sistem Ayarları -->
            <div v-if="activeMenu === 'system'">
              <v-row>
                <v-col cols="12">
                  <v-select
                    v-model="settings.dataRefreshRate"
                    :items="refreshRates"
                    label="Veri Yenileme Sıklığı"
                  ></v-select>
                </v-col>
                <v-col cols="12">
                  <v-select
                    v-model="settings.chartUpdateRate"
                    :items="refreshRates"
                    label="Grafik Güncelleme Sıklığı"
                  ></v-select>
                </v-col>
                <v-col cols="12">
                  <v-btn color="error" block>
                    Önbelleği Temizle
                    <v-icon end icon="mdi-delete"></v-icon>
                  </v-btn>
                </v-col>
              </v-row>
            </div>
          </v-card-text>

          <v-divider></v-divider>

          <v-card-actions class="pa-4">
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="saveSettings">
              Kaydet
              <v-icon end icon="mdi-content-save"></v-icon>
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script setup>
import { ref, computed } from "vue";

const activeMenu = ref("general");
const settingMenus = [
  { title: "Genel Ayarlar", icon: "mdi-cog", value: "general" },
  { title: "Alarm Ayarları", icon: "mdi-bell", value: "alerts" },
  { title: "Kullanıcı Ayarları", icon: "mdi-account", value: "user" },
  { title: "Sistem Ayarları", icon: "mdi-server", value: "system" },
];

const getCurrentMenuTitle = computed(() => {
  const menu = settingMenus.find((m) => m.value === activeMenu.value);
  return menu ? menu.title : "";
});

const languages = ["Türkçe", "English", "Deutsch"];
const timezones = [
  "(UTC+03:00) İstanbul",
  "(UTC+02:00) Athens",
  "(UTC+01:00) Berlin",
];
const refreshRates = [
  "5 saniye",
  "10 saniye",
  "30 saniye",
  "1 dakika",
  "5 dakika",
];

const settings = ref({
  language: "Türkçe",
  timezone: "(UTC+03:00) İstanbul",
  notifications: true,
  emailAlerts: true,
  smsAlerts: false,
  alertEmail: "admin@lumos.com.tr",
  alertPhone: "+90 555 123 4567",
  fullName: "Admin Kullanıcı",
  email: "admin@lumos.com.tr",
  dataRefreshRate: "30 saniye",
  chartUpdateRate: "1 dakika",
});

const saveSettings = () => {
  // Ayarları kaydetme işlemi burada yapılacak
  console.log("Ayarlar kaydediliyor:", settings.value);
};
</script>

<style scoped>
.settings-container {
  height: 100vh;
  padding: 16px;
  box-sizing: border-box;
}
</style>
