export const API_VERSION = "v1/"

export const PRIVATE_ROUTE = "private"
export const PUBLIC_ROUTE = "public"
export const MEMBERS_ROUTE = "account-service/"
export const REGISTERED_ARTIST = "registered-painter/"
export const REGISTERED_MEMBER = "registered-user/"
export const ARTIST_PERSONAL_INFO = "artist-personal-info/"
export const ARTIST_SPECIALIZATIONS = "artist-specialities/"

export const AI_MODEL = 'http://0e22-35-229-49-20.ngrok.io/v1'
export const BASIC_ACCOUNT = API_VERSION + 'account-service'
export const REGISTER_CLIENT = API_VERSION + MEMBERS_ROUTE + "register-client"
export const CURRENT_MEMBER = API_VERSION + MEMBERS_ROUTE + "current"
export const CURRENT_ARTIST = API_VERSION + MEMBERS_ROUTE + "current-painter"
export const PAINTING_PAINTER = API_VERSION + MEMBERS_ROUTE + "register-painter"
export const GET_ARTIST = API_VERSION + MEMBERS_ROUTE + 'artist-client'
export const GET_PAINTING= API_VERSION + "paintings"
export const GET_GAN_PAINTING= API_VERSION + MEMBERS_ROUTE + "gan-image"
export const REGISTER_ARTIST = API_VERSION + MEMBERS_ROUTE + "register-painter"
export const CLIENT_IMAGE = API_VERSION + MEMBERS_ROUTE + "register-client/image"

export const UPDATE_REGISTERED_MEMBER = API_VERSION + MEMBERS_ROUTE + REGISTERED_MEMBER + "update"
export const UPDATE_ARTIST_PERSONAL_INFO = API_VERSION + MEMBERS_ROUTE + ARTIST_PERSONAL_INFO + "update"
export const UPDATE_ARTIST_SPECIALIZATIONS =  API_VERSION + MEMBERS_ROUTE + ARTIST_SPECIALIZATIONS + "update"

export const SEARCH_HOME_PAGE = API_VERSION + MEMBERS_ROUTE + REGISTERED_ARTIST + "home-search"

export const SEARCH_PAGE = API_VERSION + MEMBERS_ROUTE + "paintings/" + "search"

export const SUBSCRIBE = API_VERSION + "subscribe"
