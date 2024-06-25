import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import Lost from '@/views/Lost.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(),
  routes: [{ path: '/', component: { template: '<div>Home</div>' } }],
})

describe('Lost.vue', () => {
  it('renders the 404 message', () => {
    const wrapper = mount(Lost, {
      global: {
        plugins: [router],
      },
    })
    expect(wrapper.text()).toContain('404 - Page not found')
  })

  it('contains a link to the homepage', () => {
    const wrapper = mount(Lost, {
      global: {
        plugins: [router],
      },
    })
    const homeLink = wrapper.find('a')
    expect(homeLink.text()).toBe('Go To Homepage')
  })
})
