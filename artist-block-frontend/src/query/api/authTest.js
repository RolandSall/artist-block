import configureAxios from "../axios";
import {PRIVATE_ROUTE, PUBLIC_ROUTE} from "./routes";

const axios = configureAxios();

export const getPrivate = (token,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${PRIVATE_ROUTE}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)




export const getPublic = ({ API_HOST,token }) =>{
    return axios
        .get(`${API_HOST}/${PUBLIC_ROUTE}`)
        .then(({ data }) => data)
}




