<template>
  <v-row>
    <v-col cols="12" md="6">
      <v-card>
        <v-card-title>Hoş Geldiniz!</v-card-title>
        <v-card-text>
          <p class="text-h6">Vue.js ve Vuetify ile Oluşturulmuş SPA</p>
          <p class="mt-4">
            Bu uygulama Vue CLI kullanılarak oluşturulmuş ve Vuetify 3 ile
            tasarlanmış bir Single Page Application örneğidir.
          </p>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" to="/about">
            Hakkında
            <v-icon right>mdi-arrow-right</v-icon>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-col>

    <v-col cols="12" md="6">
      <v-card>
        <v-card-title>Özellikler</v-card-title>
        <v-card-text>
          <v-list>
            <v-list-item
              prepend-icon="mdi-vuejs"
              title="Vue 3 Composition API"
            ></v-list-item>
            <v-list-item
              prepend-icon="mdi-view-dashboard"
              title="Vuetify 3 UI Framework"
            ></v-list-item>
            <v-list-item
              prepend-icon="mdi-routes"
              title="Vue Router ile SPA"
            ></v-list-item>
            <v-list-item
              prepend-icon="mdi-responsive"
              title="Responsive Tasarım"
            ></v-list-item>
          </v-list>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
// Component logic here
</script>
