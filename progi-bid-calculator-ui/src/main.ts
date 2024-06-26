import { createApp } from 'vue'
import App from '@/App.vue'
import { router } from '@/router'

import './style.css'
import 'bootstrap/scss/bootstrap.scss'
import 'bootstrap-icons/font/bootstrap-icons.css'

const app = createApp(App).use(router)

app.config.errorHandler = (err, vm, info) => {
  console.error('Global error:', err)
  router.push(`${import.meta.env.VITE_BUILD_ADDRESS}/500`)
}

router.isReady().then(() => app.mount('#app'))
