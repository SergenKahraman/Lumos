<template>
  <div class="reports-container">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="text-h5">Raporlar</div>
        <div class="text-subtitle-2 text-medium-emphasis">
          Santrallerinizin üretim ve performans raporlarını görüntüleyin ve
          analiz edin
        </div>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12" md="4">
        <v-card>
          <v-card-title>Rapor Filtresi</v-card-title>
          <v-card-text>
            <v-select
              v-model="selectedStation"
              :items="stations"
              label="Santral Seçin"
              item-title="name"
              item-value="id"
              class="mb-4"
            ></v-select>

            <v-select
              v-model="reportType"
              :items="reportTypes"
              label="Rapor Türü"
              class="mb-4"
            ></v-select>

            <v-select
              v-model="dateRange"
              :items="dateRanges"
              label="Tarih Aralığı"
              class="mb-4"
            ></v-select>

            <v-btn color="primary" block class="mt-4">
              Rapor Oluştur
              <v-icon end icon="mdi-file-chart"></v-icon>
            </v-btn>
          </v-card-text>
        </v-card>
      </v-col>

      <v-col cols="12" md="8">
        <v-card>
          <v-card-title>
            Önceki Raporlar
            <v-spacer></v-spacer>
            <v-btn color="primary" variant="text">
              Tümünü İndir
              <v-icon end icon="mdi-download"></v-icon>
            </v-btn>
          </v-card-title>
          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Rapor Adı</th>
                  <th>Santral</th>
                  <th>Tarih Aralığı</th>
                  <th>Oluşturulma Tarihi</th>
                  <th>İşlemler</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="report in reports" :key="report.id">
                  <td>{{ report.name }}</td>
                  <td>{{ report.station }}</td>
                  <td>{{ report.dateRange }}</td>
                  <td>{{ report.createdAt }}</td>
                  <td>
                    <v-btn
                      icon="mdi-eye"
                      variant="text"
                      color="primary"
                      class="mr-2"
                    ></v-btn>
                    <v-btn
                      icon="mdi-download"
                      variant="text"
                      color="primary"
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
import { ref } from "vue";

const selectedStation = ref(null);
const reportType = ref(null);
const dateRange = ref(null);

const stations = ref([
  { id: 1, name: "GES-01 Ankara" },
  { id: 2, name: "GES-02 İzmir" },
  { id: 3, name: "GES-03 Antalya" },
]);

const reportTypes = [
  "Üretim Raporu",
  "Performans Raporu",
  "Alarm Raporu",
  "Bakım Raporu",
];

const dateRanges = [
  "Bugün",
  "Son 7 Gün",
  "Bu Ay",
  "Son 3 Ay",
  "Bu Yıl",
  "Özel Aralık",
];

const reports = ref([
  {
    id: 1,
    name: "Aylık Üretim Raporu",
    station: "GES-01 Ankara",
    dateRange: "Ocak 2024",
    createdAt: "02.02.2024",
  },
  {
    id: 2,
    name: "Performans Analizi",
    station: "GES-02 İzmir",
    dateRange: "Son 3 Ay",
    createdAt: "01.02.2024",
  },
  {
    id: 3,
    name: "Alarm Özeti",
    station: "Tüm Santraller",
    dateRange: "Ocak 2024",
    createdAt: "01.02.2024",
  },
]);
</script>

<style scoped>
.reports-container {
  height: 100vh;
  padding: 16px;
  box-sizing: border-box;
}
</style>
