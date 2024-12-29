<template>
  <div class="dashboard-container">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="text-h5">Gösterge Paneli</div>
        <div class="text-subtitle-2 text-medium-emphasis">
          Güneş enerjisi santrallerinizin anlık performans ve üretim verileri
        </div>
      </v-col>
    </v-row>

    <v-row class="mb-2">
      <!-- Toplam Üretim Kartı -->
      <v-col cols="12" md="3">
        <v-card height="100">
          <v-card-text class="d-flex align-center justify-space-between h-100">
            <div>
              <div class="text-h5 mb-1">125.4 MWh</div>
              <div class="text-subtitle-2">Günlük Üretim</div>
              <div class="text-caption text-medium-emphasis">Hedefin %95'i</div>
            </div>
            <v-icon
              size="36"
              color="success"
              icon="mdi-lightning-bolt"
            ></v-icon>
          </v-card-text>
        </v-card>
      </v-col>

      <!-- Aktif Santral Sayısı -->
      <v-col cols="12" md="3">
        <v-card height="100">
          <v-card-text class="d-flex align-center justify-space-between h-100">
            <div>
              <div class="text-h5 mb-1">12</div>
              <div class="text-subtitle-2">Aktif Santral</div>
              <div class="text-caption text-success">Tümü Çalışıyor</div>
            </div>
            <v-icon size="36" color="primary" icon="mdi-solar-power"></v-icon>
          </v-card-text>
        </v-card>
      </v-col>

      <!-- Performans Oranı -->
      <v-col cols="12" md="3">
        <v-card height="100">
          <v-card-text class="d-flex align-center justify-space-between h-100">
            <div>
              <div class="text-h5 mb-1">97.3%</div>
              <div class="text-subtitle-2">Performans Oranı</div>
              <div class="text-caption text-info">+2.1% Geçen Aya Göre</div>
            </div>
            <v-icon size="36" color="info" icon="mdi-chart-line"></v-icon>
          </v-card-text>
        </v-card>
      </v-col>

      <!-- Aktif Alarm -->
      <v-col cols="12" md="3">
        <v-card height="100">
          <v-card-text class="d-flex align-center justify-space-between h-100">
            <div>
              <div class="text-h5 mb-1">3</div>
              <div class="text-subtitle-2">Aktif Alarm</div>
              <div class="text-caption text-warning">2 Kritik, 1 Uyarı</div>
            </div>
            <v-icon size="36" color="warning" icon="mdi-alert"></v-icon>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row class="mb-2" style="height: calc(100vh - 450px)">
      <v-col cols="12" md="8" class="h-100">
        <v-card class="h-100">
          <v-card-title class="text-subtitle-1"
            >Santral Üretim Karşılaştırması</v-card-title
          >
          <v-card-text class="h-100 pb-0">
            <ProductionChart />
          </v-card-text>
        </v-card>
      </v-col>

      <!-- Alarmlar -->
      <v-col cols="12" md="4" class="h-100">
        <v-card class="h-100">
          <v-card-title class="d-flex align-center text-subtitle-1">
            Son Alarmlar
            <v-chip color="error" size="x-small" class="ml-2">Yeni</v-chip>
            <v-spacer></v-spacer>
            <v-btn color="error" size="small" variant="text" to="/alerts">
              Tümünü Gör
              <v-icon end icon="mdi-arrow-right"></v-icon>
            </v-btn>
          </v-card-title>
          <v-card-text class="pa-0">
            <v-list density="compact" class="alarm-list">
              <v-list-item
                v-for="alarm in alarms"
                :key="alarm.id"
                :title="alarm.title"
                :subtitle="alarm.location"
                :prepend-icon="alarm.icon"
              >
                <template v-slot:append>
                  <v-chip :color="alarm.severity" size="x-small">{{
                    alarm.type
                  }}</v-chip>
                </template>
              </v-list-item>
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <!-- Santral Listesi -->
      <v-col cols="12">
        <v-card>
          <v-card-title class="d-flex align-center text-subtitle-1">
            Santral Durumları
            <v-chip color="success" size="x-small" class="ml-2">Canlı</v-chip>
            <v-spacer></v-spacer>
            <v-btn color="primary" size="small" variant="text" to="/stations">
              Tümünü Gör
              <v-icon end icon="mdi-arrow-right"></v-icon>
            </v-btn>
          </v-card-title>
          <v-card-text class="pa-0">
            <v-table density="compact">
              <thead>
                <tr>
                  <th>Santral Adı</th>
                  <th>Günlük Üretim</th>
                  <th>Durum</th>
                  <th>Performans</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="station in stations" :key="station.id">
                  <td>
                    <div class="d-flex align-center">
                      <router-link
                        :to="'/stations/' + station.id"
                        class="text-decoration-none text-primary"
                      >
                        {{ station.name }}
                      </router-link>
                    </div>
                  </td>
                  <td>{{ station.production }} MWh</td>
                  <td>
                    <v-chip :color="station.status.color" size="x-small">{{
                      station.status.text
                    }}</v-chip>
                  </td>
                  <td>
                    <div class="d-flex align-center">
                      {{ station.performance }}%
                      <v-icon
                        :color="station.trend.color"
                        size="small"
                        class="ml-1"
                        >{{ station.trend.icon }}</v-icon
                      >
                    </div>
                  </td>
                </tr>
              </tbody>
            </v-table>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script setup>
import ProductionChart from "../components/ProductionChart.vue";

const alarms = [
  {
    id: 1,
    title: "Inverter Hatası",
    location: "GES-01 Ankara - Panel Grubu 3",
    icon: "mdi-alert",
    severity: "error",
    type: "Kritik",
  },
  {
    id: 2,
    title: "Düşük Performans",
    location: "GES-02 İzmir - Panel Grubu 1",
    icon: "mdi-alert",
    severity: "warning",
    type: "Uyarı",
  },
  {
    id: 3,
    title: "Bağlantı Hatası",
    location: "GES-03 Antalya - Veri Toplama Ünitesi",
    icon: "mdi-alert",
    severity: "error",
    type: "Kritik",
  },
];

const stations = [
  {
    id: 1,
    name: "GES-01 Ankara",
    production: 24.5,
    status: { color: "success", text: "Aktif" },
    performance: 98,
    trend: { color: "success", icon: "mdi-trending-up" },
  },
  {
    id: 2,
    name: "GES-02 İzmir",
    production: 18.2,
    status: { color: "success", text: "Aktif" },
    performance: 95,
    trend: { color: "info", icon: "mdi-trending-neutral" },
  },
  {
    id: 3,
    name: "GES-03 Antalya",
    production: 22.8,
    status: { color: "error", text: "Bakımda" },
    performance: 0,
    trend: { color: "error", icon: "mdi-trending-down" },
  },
];
</script>

<style scoped>
.dashboard-container {
  height: 100vh;
  padding: 16px;
  box-sizing: border-box;
}

.h-100 {
  height: 100%;
}

.alarm-list {
  max-height: calc(100% - 48px);
  overflow-y: auto;
}

:deep(.v-card-text) {
  padding: 12px;
}

:deep(.v-card-title) {
  padding: 12px;
}

.text-decoration-none {
  text-decoration: none;
}
</style>
