import {SEARCH_HOME_PAGE, SEARCH_PAGE} from "./routes";
import axios from "axios";
import qs from "qs";

const headers = (token) => {
    return token?    {
        Authorization: `Bearer ${token}`
    } : {}
}

export const searchHomePage = (search,location,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${SEARCH_HOME_PAGE}`, {
            params: {
                SearchCriteria: search,
                Location: location
            }
        })
        .then(({data}) => data)

export const searchPage = ({pageNum, search, year, rate, pageSize},{ API_HOST },token) => {
    console.log(rate)
    return axios
        .get(`${API_HOST}/${SEARCH_PAGE}`, {
            headers: headers(token),
            params: {
                PageNumber: pageNum,
                PaintingDescription: search,
                PaintingYear: year,
                RateStart: rate[0],
                RateEnd: rate[1],
                PageSize: pageSize
            },
            paramsSerializer: params => {
                return qs.stringify(params)
            }
        })
        .then(({data, headers}) => {
            return {artists: data, pagination: JSON.parse(headers['x-pagination'])}
        })
}