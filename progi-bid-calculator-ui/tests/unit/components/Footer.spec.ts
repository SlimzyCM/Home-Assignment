import { mount } from '@vue/test-utils'
import { expect, test, describe } from 'vitest'
import Footer from '@/components/Footer.vue'

describe('Footer.vue', () => {
  test('renders footer component', () => {
    const wrapper = mount(Footer)
    expect(wrapper.text()).toContain('Copyright © 2024 - Progi Bid Calculator')
  })
})
