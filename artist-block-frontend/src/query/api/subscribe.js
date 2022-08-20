import {SUBSCRIBE} from "./routes";
import axios from "axios";

const headers = (token) => {
    return token?    {
        Authorization: `Bearer ${token}`
    } : {}
}

export const subscribe = (email,{ API_HOST }) =>
    axios
        .post(`${API_HOST}/${SUBSCRIBE}`, {
        email
        })
        .then(({data}) => data)