import configureAxios from "../axios";
import {
    CLIENT_IMAGE,
    CURRENT_ARTIST,
    CURRENT_MEMBER,
    GET_ARTIST,
    REGISTER_CLIENT,
    REGISTER_ARTIST,
    UPDATE_ARTIST_PERSONAL_INFO,
    UPDATE_ARTIST_SPECIALIZATIONS,
    UPDATE_REGISTERED_MEMBER,
    GET_PAINTING,
    GET_GAN_PAINTING,
    PAINTING_PAINTER
} from "./routes";
import * as Api from "./index";

const axios = configureAxios();

export const getCurrentMember = (token,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${CURRENT_MEMBER}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)

export const getCurrentArtist = (token,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${CURRENT_ARTIST}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)

export const getPaintingPainter = (token, paintingId ,{  API_HOST }) =>
    axios
        .get(`${API_HOST}/${PAINTING_PAINTER}/${paintingId}`, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)

export const getArtist = (token, artistId,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${GET_ARTIST}/${artistId}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        .then(({ data }) => data)


export const getOwnPaintings = (token ,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/v1/account-service/current/paintings`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        .then(({ data }) => data)


export const getPainterPaintings = (token ,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/v1/account-service/current-painter/paintings`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        .then(({ data }) => data)

export const getGanPaintings = (token ,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/v1/account-service/gan-image/collection`,
            {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            })
        .then(({ data }) => data)



export const useThreePainting = (token ,{ API_HOST })  => {
    console.log(213123123123)
    // return axios
    //     .get(`${API_HOST}/v1/account-service/gan-image/collection`,
    //         {
    //             headers: {
    //                 Authorization: `Bearer ${token}`
    //             }
    //         })
    //     .then(({ data }) => data)
}


export const getPainting = (artistId,{ API_HOST }) => {
    console.log(`${API_HOST}/${GET_PAINTING}/${artistId}`)
    return axios
        .get(`${API_HOST}/${GET_PAINTING}/${artistId}`)
        .then(({data}) => data)
}


export const getGanPainting = (artistId,{ API_HOST }) => {
    console.log(`${API_HOST}/${GET_GAN_PAINTING}/${artistId}`)
    return axios
        .get(`${API_HOST}/${GET_GAN_PAINTING}/${artistId}`)
        .then(({data}) => data)
}


export const getClientImage = (token,{ API_HOST }) =>
    axios
        .get(`${API_HOST}/${CLIENT_IMAGE}`,  {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({data}) => data)


export const updateArtist = (token,payload,{ API_HOST }) =>
    axios
        .put(`${API_HOST}/${UPDATE_ARTIST_PERSONAL_INFO}`, payload,{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)



export const updateArtistSpecializations= (token,payload,{ API_HOST }) =>
    axios
        .put(`${API_HOST}/${UPDATE_ARTIST_SPECIALIZATIONS}`, payload,{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)





export const updateMember = (token,payload,{ API_HOST }) =>
    axios
        .put(`${API_HOST}/${UPDATE_REGISTERED_MEMBER}`, {...payload},{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)

