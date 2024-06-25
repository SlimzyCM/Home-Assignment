import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import Error from '@/views/Error.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(),
  routes: [{ path: '/', component: { template: '<div>Home</div>' } }],
})

describe('Error.vue', () => {
  it('renders the error message', () => {
    const wrapper = mount(Error, {
      global: {
        plugins: [router],
      },
    })
    expect(wrapper.text()).toContain(
      'An error occurred while processing your request'
    )
  })

  it('contains a link to the homepage', () => {
    const wrapper = mount(Error, {
      global: {
        plugins: [router],
      },
    })
    const homeLink = wrapper.find('a')
    expect(homeLink.text()).toBe('Go To Homepage')
  })
})
