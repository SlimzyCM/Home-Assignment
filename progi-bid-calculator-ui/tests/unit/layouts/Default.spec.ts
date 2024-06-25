import { mount } from '@vue/test-utils'
import { expect, test, describe, vi } from 'vitest'
import Default from '@/layouts/Default.vue'

vi.mock('@/components/Nav.vue', () => ({
  default: { template: '<div>Nav Component</div>' },
}))
vi.mock('@/components/BidCalculator.vue', () => ({
  default: { template: '<div>BidCalculator Component</div>' },
}))
vi.mock('@/components/Footer.vue', () => ({
  default: { template: '<div>Footer Component</div>' },
}))

describe('Default.vue', () => {
  test('renders default layout with all components', () => {
    const wrapper = mount(Default)
    expect(wrapper.text()).toContain('Nav Component')
    expect(wrapper.text()).toContain('BidCalculator Component')
    expect(wrapper.text()).toContain('Footer Component')
  })
})
