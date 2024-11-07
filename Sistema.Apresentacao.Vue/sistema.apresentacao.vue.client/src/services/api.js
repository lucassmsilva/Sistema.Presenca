import axios from 'axios';
import Result from './result';

export class Api {
    constructor(baseURL, config = {}) {
        this.instance = axios.create({
            baseURL,
            ...config
        });

        this.baseURL = baseURL;

        // Add request interceptor
        this.instance.interceptors.request.use(
            (config) => {
                // You can add auth headers or other configurations here
                return config;
            },
            (error) => Promise.reject(error)
        );
    }

    async request(method, endpoint, { params = null, data = null } = {}) {
        let url = `${this.baseURL}${endpoint}`;

        console.log(url)

        if (params) {
            const searchParams = new URLSearchParams();

            // Process each parameter
            Object.entries(params).forEach(([key, value]) => {
                if (Array.isArray(value)) {
                    // Handle array parameters for ASP.NET
                    value.forEach(item => {
                        searchParams.append(key, item.toString());
                    });
                } else {
                    searchParams.append(key, value?.toString() ?? '');
                }
            });

            const queryString = searchParams.toString();
            if (queryString) {
                url += `?${queryString}`;
            }
        }
        try {
            const response = await this.instance({ method, url, data });
            return Result.fromAxiosResponse(response);
        } catch (error) {
            return Result.fromAxiosError(error);
        }
    }

    // Convenience methods for different HTTP verbs
    get(endpoint, params = null) {
        return this.request('get', endpoint, { params });
    }

    post(endpoint, data = null) {
        return this.request('post', endpoint, { data });
    }

    put(endpoint, data = null) {
        return this.request('put', endpoint, { data });
    }

    delete(endpoint, params = null) {
        return this.request('delete', endpoint, { params });
    }
}

// Create and export default instance
const api = new Api('https://localhost:7247/api/');
export default api;