<template>
  <div class="stations-container">
    <v-row class="mb-2">
      <v-col cols="12">
        <div class="text-h5">Santraller</div>
        <div class="text-subtitle-2 text-medium-emphasis">
          Güneş enerjisi santrallerinizin detaylı bilgileri ve yönetimi
        </div>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-card>
          <v-card-title class="d-flex align-center">
            Tüm Santraller
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              append-icon="mdi-magnify"
              label="Arama"
              single-line
              hide-details
              density="compact"
              class="ml-4"
              style="max-width: 300px"
            ></v-text-field>
          </v-card-title>

          <v-card-text>
            <v-table>
              <thead>
                <tr>
                  <th>Santral Adı</th>
                  <th>Konum</th>
                  <th>Kurulu Güç</th>
                  <th>Durum</th>
                  <th>Günlük Üretim</th>
                  <th>İşlemler</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="station in stations" :key="station.id">
                  <td>{{ station.name }}</td>
                  <td>{{ station.location }}</td>
                  <td>{{ station.power }} MWp</td>
                  <td>
                    <v-chip
                      :color="station.status === 'active' ? 'success' : 'error'"
                      size="small"
                    >
                      {{ station.status === "active" ? "Aktif" : "Bakımda" }}
                    </v-chip>
                  </td>
                  <td>{{ station.dailyProduction }} MWh</td>
                  <td>
                    <v-btn
                      icon="mdi-chart-box"
                      variant="text"
                      color="primary"
                      class="mr-2"
                      :to="'/stations/' + station.id + '/reports'"
                    ></v-btn>
                    <v-btn
                      icon="mdi-cog"
                      variant="text"
                      color="primary"
                      :to="'/stations/' + station.id + '/settings'"
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

const search = ref("");
const stations = ref([
  {
    id: 1,
    name: "GES-01 Ankara",
    location: "Ankara, Polatlı",
    power: 10,
    status: "active",
    dailyProduction: 24.5,
  },
  {
    id: 2,
    name: "GES-02 İzmir",
    location: "İzmir, Bergama",
    power: 8,
    status: "active",
    dailyProduction: 18.2,
  },
  {
    id: 3,
    name: "GES-03 Antalya",
    location: "Antalya, Serik",
    power: 12,
    status: "maintenance",
    dailyProduction: 0,
  },
]);
</script>

<style scoped>
.stations-container {
  height: 100vh;
  padding: 16px;
  box-sizing: border-box;
}
</style>
