import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import colors from 'vuetify/lib/util/colors'

export default defineNuxtPlugin((app) => {
  const vuetify = createVuetify({
    defaults: {
      global: {
      
      },
      Vsheet: {
        eleveation: 4,

      },
    },
    theme: {
      defaultTheme: 'light',
      themes: {
        light: {
          colors: {   
            'brand': colors.indigo.darken4,
            //surface: colors.brown.darken1,  
          }
        }, 
        dark :{
          colors: {
            'brand': colors.indigo.darken4,
          }
        }
      }
    }
  })
  app.vueApp.use(vuetify)
})