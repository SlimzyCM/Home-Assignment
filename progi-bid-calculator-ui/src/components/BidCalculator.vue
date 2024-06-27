<template>
  <div class="box p-5 rounded-3">
    <div class="row gx-5">
      <div class="col-md-6">
        <div class="text-xxl-start">
          <div class="fs-8 fw-bold text-uppercase">Account / Tools /</div>
          <h1 class="display-5 fw-bold mb-5">
            <span class="text-gradient d-inline">Bid Calculator</span>
          </h1>
          <div class="form-group mb-4">
            <label class="fs-8 mb-2 fw-bold text-uppercase" for="vehicleType"
              >Vehicle Type</label
            >
            <select
              id="vehicleType"
              class="form-control w-65"
              v-model="vehicleType"
              @blur="calculateBid"
            >
              <option :value="VehicleType.Common">Common</option>
              <option :value="VehicleType.Luxury">Luxury</option>
            </select>
          </div>

          <div class="form-group mb-5">
            <label class="fs-8 mb-2 fw-bold text-uppercase" for="basePrice"
              >Vehicle Base Price</label
            >
            <div class="input-group w-65">
              <span class="input-group-text">$</span>
              <input
                id="basePrice"
                v-model="basePrice"
                type="number"
                min="0"
                step="0.01"
                class="form-control"
                aria-label="Amount (to the nearest dollar)"
                @blur="calculateBid"
                @input="errors = []"
              />
            </div>
            <span class="text-danger d-block small"></span>
          </div>
          <div v-if="errors.length > 0" class="alert alert-danger mt-3">
            <ul class="mb-0">
              <li v-for="error in errors" :key="error">{{ error }}</li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-md-6 m-md-0 p-md-0">
        <div class="d-flex mt-5 mt-md-0">
          <div>
            <div class="adjustable-text fw-bold text-uppercase">
              THE TOTAL COST CALCULATED FOR THE VEHICLE BIDDING
            </div>
            <h1 class="display-5 fw-bold mb-4">
              <span class="text-gradient d-inline">
                ${{ bidResult ? bidResult.totalPrice.toFixed(2) : '0.00' }}
              </span>
            </h1>
            <div>
              <ul class="list-group fw-bold adjustable-list">
                <li
                  class="list-unstyled d-flex justify-content-start align-items-center mb-2"
                >
                  <span class="badge badge-info text-bg-info badge-pill me-2"
                    >Basic User Fee:
                  </span>
                  <span
                    >${{
                      bidResult ? bidResult.basicUserFee.toFixed(2) : '0.00'
                    }}</span
                  >
                </li>
                <li
                  class="list-unstyled d-flex justify-content-start align-items-center mb-2"
                >
                  <span class="badge badge-info text-bg-info badge-pill me-2"
                    >Seller Special Fee:
                  </span>
                  <span
                    >${{
                      bidResult ? bidResult.sellerSpecialFee.toFixed(2) : '0.00'
                    }}</span
                  >
                </li>
                <li
                  class="list-unstyled d-flex justify-content-start align-items-center mb-2"
                >
                  <span class="badge badge-info text-bg-info badge-pill me-2"
                    >Association Fee:</span
                  >
                  <span
                    >${{
                      bidResult ? bidResult.associationFee.toFixed(2) : '0.00'
                    }}</span
                  >
                </li>
                <li
                  class="list-unstyled d-flex justify-content-start align-items-center mb-2"
                >
                  <span class="badge badge-info text-bg-info badge-pill me-2"
                    >Storage Fee:</span
                  >
                  <span
                    >${{
                      bidResult ? bidResult.storageFee.toFixed(2) : '0.00'
                    }}</span
                  >
                </li>
              </ul>
            </div>
            <div class="mt-3">
              <ul class="list-group fs-9 fw-bold adjustable-list">
                <li
                  class="list-unstyled d-flex justify-content-between align-items-start mb-3"
                >
                  <span
                    class="badge badge-info text-bg-danger badge-pill me-2 mt-2"
                    >1</span
                  >
                  <span>
                    At our auction house, we handle the complex bid calculations
                    behind the scenes. This calculator demonstrates how we
                    process various fees to determine the final bid amount.
                  </span>
                </li>
                <li
                  class="list-unstyled d-flex justify-content-between align-items-start mb-3"
                >
                  <span
                    class="badge badge-info text-bg-danger badge-pill me-2 mt-2"
                    >2</span
                  >
                  <span>
                    To maximize your bidding strategy, we recommend utilizing
                    our integrated market analysis tools. This will help you
                    make informed decisions, ensuring you're bidding
                    competitively while staying within your budget.
                  </span>
                </li>
              </ul>
            </div>
            <button class="btn bg-secondary text-white-50 mt-2">
              Consult With Our Team
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import axios from 'axios'
import axiosClient from '../utils/api'
enum VehicleType {
  Common = 0,
  Luxury = 1,
}
interface BidResult {
  basePrice: number
  vehicleType: string
  basicUserFee: number
  sellerSpecialFee: number
  associationFee: number
  storageFee: number
  totalPrice: number
}

const basePrice = ref<number>(0)
const vehicleType = ref<VehicleType>(VehicleType.Common)
const bidResult = ref<BidResult | null>(null)
const errors = ref<string[]>([])

const calculateBid = async () => {
  errors.value = [] // Clear previous errors
  console.log('called')
  if (basePrice.value <= 0) {
    errors.value.push('Base price must be greater than 0')
    return
  }

  if (vehicleType.value === undefined) {
    errors.value.push('Please select a vehicle type')
    return
  }

  try {
    const response = await axiosClient.post<BidResult>(
      'Calculator/CalculateBid',
      {
        price: basePrice.value,
        vehicleType: vehicleType.value,
      }
    )
    bidResult.value = response.data
  } catch (error) {
    if (axios.isAxiosError(error) && error.response) {
      const responseData = error.response.data
      if (responseData.errors) {
        Object.values(responseData.errors).forEach((errorArray: any) => {
          if (Array.isArray(errorArray)) {
            errors.value.push(...errorArray)
          } else if (typeof errorArray === 'string') {
            errors.value.push(errorArray)
          }
        })
      } else if (responseData.detail) {
        errors.value.push(responseData.detail)
      } else {
        errors.value.push('An error occurred while calculating the bid')
      }
    } else {
      errors.value.push('An unexpected error occurred')
    }
    bidResult.value = null
  }
}
</script>
