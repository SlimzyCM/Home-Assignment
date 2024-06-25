import { expect, test, describe } from 'vitest'
import { routes } from '@/router'

describe('Router', () => {
  test('has route for home page', () => {
    const homeRoute = routes.find(
      (route) => route.path === `${import.meta.env.VITE_BUILD_ADDRESS}/`
    )
    expect(homeRoute).toBeTruthy()
  })

  test('has route for 500 error page', () => {
    const serverErrorRoute = routes.find(
      (route) => route.path === `${import.meta.env.VITE_BUILD_ADDRESS}/500`
    )
    expect(serverErrorRoute).toBeTruthy()
    expect(serverErrorRoute?.name).toBe('ServerError')
  })

  test('has catch-all route for 404 page', () => {
    const notFoundRoute = routes.find(
      (route) => route.path === '/:pathMatch(.*)*'
    )
    expect(notFoundRoute).toBeTruthy()
    expect(notFoundRoute?.name).toBe('NotFound')
  })
})
