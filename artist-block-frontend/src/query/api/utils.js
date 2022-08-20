import axios from 'axios';

const httpClient = (API_HOST) => axios.create({
    baseURL: API_HOST,
});

export {httpClient};
