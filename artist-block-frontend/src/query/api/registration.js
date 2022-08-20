import configureAxios from "../axios";
import {CLIENT_IMAGE, REGISTER_CLIENT, REGISTER_ARTIST, BASIC_ACCOUNT, AI_MODEL} from "./routes";

const axios = configureAxios();

export const registerClient = (token,payload,{ API_HOST }) => {
    return axios
        .post(`${API_HOST}/${REGISTER_CLIENT}`, {...payload}, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({data}) => data)
}

export const buyPainting = (token,paintingId,{ API_HOST }) => {
    console.log(token)
    return axios
        .post(`${API_HOST}/${BASIC_ACCOUNT}/buy/${paintingId}`,null, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({data}) => data)
}

export const createGan = (description,{ API_HOST }) => {
    return fetch(`${AI_MODEL}/generate_text2img`+
        '?' + new URLSearchParams({
            prompt: description,
            width:256,
            height:256
    }),{
        method: 'POST',

    }).then(response => response.blob())
        .then(blob => ({
            image:URL.createObjectURL(blob),
            blob
        }) )
        // .then(blob => URL.createObjectURL(blob) )
}

export const getGanImgProgress = ({ API_HOST }) => {
    return fetch(`${AI_MODEL}/generate_text2img_progressimg`,{
        method: 'POST'
    })
        .then(response => response.blob())
        .then(blob => ({
            image:URL.createObjectURL(blob),
            blob
        }) )
    // return axios
    //     .post(`${AI_MODEL}/generate_text2img_progressimg`)
    //     .then(({data}) => data)
}

export const getGanProgressBar = ({ API_HOST }) => {
    return axios
        .post(`${AI_MODEL}/generate_text2img_progressbar`)
        .then(({data}) => data)
}

export const postClientImage = (token,formData,{ API_HOST }) => {
    return axios
        .post(`${API_HOST}/${CLIENT_IMAGE}`, formData, {
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'multipart/form-data'
            }
        })
        .then(({data}) => data)
}

export const registerArtist = (token,payload,{ API_HOST }) =>
    axios
        .post(`${API_HOST}/${REGISTER_ARTIST}`, {...payload},{
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
        .then(({ data }) => data)



export const postPaintingImage = (token,paintingId,formData,{ API_HOST }) => {
    console.log(token,paintingId,formData, formData.get("image"))
    return axios
        .post(`https://artist-block-account-service.herokuapp.com/api/v1/create-painting/${paintingId}`, formData, {
            headers: {
                Authorization: `Bearer ${token}`,
                'Content-Type': 'multipart/form-data'
            }
        })
        .then(({data}) => data)
}

export const postPainting = (token,painterId,payload,{ API_HOST }) => {
    return axios
        .post(`https://artist-block-account-service.herokuapp.com/api/v1/create-painting/${painterId}`, payload, {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
        .then(({data}) => data)
}