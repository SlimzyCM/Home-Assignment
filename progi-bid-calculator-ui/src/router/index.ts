import { createRouter, createWebHistory } from 'vue-router'

const baseUrl = import.meta.env.VITE_BUILD_ADDRESS || ''

export const routes = [
  {
    path: `/${baseUrl}`,
    component: () => import('@/layouts/Default.vue'),
  },
  {
    path: `/${baseUrl}500`,
    name: 'ServerError',
    component: () => import('@/views/Error.vue'),
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/Lost.vue'),
  },
]

export const router = createRouter({
  history: createWebHistory(),
  routes: routes,
})

router.onError((error) => {
  console.error('Navigation error:', error)
  router.push(`/${baseUrl}500`)
})
