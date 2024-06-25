
import axios from 'axios';
import { router } from '@/router';

const baseUrl = import.meta.env.VITE_BUILD_ADDRESS || ''

const api = axios.create({
    baseURL: 'https://localhost:7118/api/v1/'
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 500) {
      router.push(`${baseUrl}/500`);
    }
    return Promise.reject(error);
  }
);

export default api;