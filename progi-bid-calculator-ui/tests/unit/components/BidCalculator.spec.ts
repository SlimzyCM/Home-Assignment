import { mount } from '@vue/test-utils'
import { expect, test, describe, vi, beforeEach } from 'vitest'
import BidCalculator from '@/components/BidCalculator.vue'
import axiosClient from '@/utils/api'

vi.mock('@/utils/api')

describe('BidCalculator.vue', () => {
  let wrapper: any

  beforeEach(() => {
    wrapper = mount(BidCalculator)
  })

  test('renders bid calculator component', () => {
    expect(wrapper.text()).toContain('Bid Calculator')
  })

  test('calculates bid when inputs change and blur', async () => {
    const mockResponse = {
      data: {
        basePrice: 1000,
        vehicleType: 'Common',
        basicUserFee: 100,
        sellerSpecialFee: 20,
        associationFee: 10,
        storageFee: 100,
        totalPrice: 1230,
      },
    }
    vi.mocked(axiosClient.post).mockResolvedValue(mockResponse)

    const basePrice = wrapper.find('#basePrice')
    const vehicleType = wrapper.find('#vehicleType')

    await basePrice.setValue('1000')
    await vehicleType.setValue('0') // Common
    await basePrice.trigger('blur')

    await wrapper.vm.$nextTick()

    expect(wrapper.text()).toContain('$1230.00')
  })

  test('displays error message on API error', async () => {
    vi.mocked(axiosClient.post).mockRejectedValue(new Error('API Error'))

    const basePrice = wrapper.find('#basePrice')

    await basePrice.setValue('1000')
    await basePrice.trigger('blur')

    await wrapper.vm.$nextTick()

    expect(wrapper.text()).toContain('An unexpected error occurred')
  })

  test('displays error for invalid input', async () => {
    const basePrice = wrapper.find('#basePrice')

    await basePrice.setValue('-100')
    await basePrice.trigger('blur')

    await wrapper.vm.$nextTick()

    expect(wrapper.text()).toContain('Base price must be greater than 0')
  })
})
