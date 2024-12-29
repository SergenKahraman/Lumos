import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'customTheme',
    themes: {
      customTheme: {
        dark: false,
        colors: {
          primary: '#2E5077',     // Ana renk - Koyu mavi
          secondary: '#5C89B5',   // Açık mavi
          accent: '#E49B3B',      // Turuncu - Vurgu rengi
          info: '#4A90E2',        // Bilgi rengi - Parlak mavi
          success: '#4CAF7A',     // Başarı rengi - Yeşil
          warning: '#F7B267',     // Uyarı rengi - Açık turuncu
          error: '#D64045',       // Hata rengi - Kırmızı
          background: '#F5F7FA',  // Arka plan rengi - Açık gri
          surface: '#FFFFFF',     // Yüzey rengi - Beyaz
        }
      }
    }
  }
})

const app = createApp(App)
app.use(router)
app.use(vuetify)
app.mount('#app')
