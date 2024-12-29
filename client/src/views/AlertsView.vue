<template>
  <div class="alerts-container">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="text-h5">Alarmlar</div>
        <div class="text-subtitle-2 text-medium-emphasis">
          Santrallerinizin alarm ve uyarılarını takip edin ve yönetin
        </div>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title class="d-flex align-center">
            Alarm Listesi
            <v-spacer></v-spacer>
            <v-btn-group class="mr-4">
              <v-btn
                :color="selectedStatus === 'all' ? 'primary' : undefined"
                @click="selectedStatus = 'all'"
              >
                Tümü ({{ alarms.length }})
              </v-btn>
              <v-btn
                :color="selectedStatus === 'critical' ? 'error' : undefined"
                @click="selectedStatus = 'critical'"
              >
                Kritik ({{ criticalCount }})
              </v-btn>
              <v-btn
                :color="selectedStatus === 'warning' ? 'warning' : undefined"
                @click="selectedStatus = 'warning'"
              >
                Uyarı ({{ warningCount }})
              </v-btn>
            </v-btn-group>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Arama"
              single-line
              hide-details
              density="compact"
              style="max-width: 300px"
            ></v-text-field>
          </v-card-title>

          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Tarih</th>
                  <th>Santral</th>
                  <th>Alarm Tipi</th>
                  <th>Mesaj</th>
                  <th>Durum</th>
                  <th>İşlemler</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="alarm in filteredAlarms" :key="alarm.id">
                  <td>{{ alarm.date }}</td>
                  <td>{{ alarm.station }}</td>
                  <td>
                    <v-chip
                      :color="alarm.type === 'critical' ? 'error' : 'warning'"
                      size="small"
                    >
                      {{ alarm.type === "critical" ? "Kritik" : "Uyarı" }}
                    </v-chip>
                  </td>
                  <td>{{ alarm.message }}</td>
                  <td>
                    <v-chip
                      :color="alarm.status === 'active' ? 'error' : 'success'"
                      size="small"
                    >
                      {{ alarm.status === "active" ? "Aktif" : "Çözüldü" }}
                    </v-chip>
                  </td>
                  <td>
                    <v-btn
                      icon="mdi-eye"
                      variant="text"
                      color="primary"
                      class="mr-2"
                    ></v-btn>
                    <v-btn
                      :icon="
                        alarm.status === 'active' ? 'mdi-check' : 'mdi-refresh'
                      "
                      variant="text"
                      :color="alarm.status === 'active' ? 'success' : 'primary'"
                    ></v-btn>
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
import { ref, computed } from "vue";

const search = ref("");
const selectedStatus = ref("all");

const alarms = ref([
  {
    id: 1,
    date: "2024-02-02 10:15",
    station: "GES-01 Ankara",
    type: "critical",
    message: "Inverter Hatası - Panel Grubu 3",
    status: "active",
  },
  {
    id: 2,
    date: "2024-02-02 09:30",
    station: "GES-02 İzmir",
    type: "warning",
    message: "Düşük Performans - Panel Grubu 1",
    status: "active",
  },
  {
    id: 3,
    date: "2024-02-02 08:45",
    station: "GES-03 Antalya",
    type: "critical",
    message: "Bağlantı Hatası - Veri Toplama Ünitesi",
    status: "active",
  },
  {
    id: 4,
    date: "2024-02-01 15:20",
    station: "GES-01 Ankara",
    type: "warning",
    message: "Sıcaklık Yüksek - İnvertör 2",
    status: "resolved",
  },
]);

const criticalCount = computed(
  () => alarms.value.filter((a) => a.type === "critical").length
);

const warningCount = computed(
  () => alarms.value.filter((a) => a.type === "warning").length
);

const filteredAlarms = computed(() => {
  let filtered = alarms.value;

  if (selectedStatus.value !== "all") {
    filtered = filtered.filter((a) => a.type === selectedStatus.value);
  }

  if (search.value) {
    const searchLower = search.value.toLowerCase();
    filtered = filtered.filter(
      (a) =>
        a.station.toLowerCase().includes(searchLower) ||
        a.message.toLowerCase().includes(searchLower)
    );
  }

  return filtered;
});
</script>

<style scoped>
.alerts-container {
  height: 100vh;
  padding: 16px;
  box-sizing: border-box;
}
</style>
