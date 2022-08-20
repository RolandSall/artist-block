export const PRIVATE_AUTH_KEY = 'private'
export const PUBLIC_AUTH_KEY = 'public'
export const CURRENT_MEMBER_KEY = 'current_member'
export const SEARCH_HOME_KEY = 'search_home'
export const HIRE_ARTIST_SEARCH_KEY = 'hire_artist_search'
export const CURRENT_ARTIST_KEY = 'current_artist'
export const PAINTING_PAINTER = 'painting_painter'
export const OWN_PAINTING_KEY = 'own-painting-key'
export const PAINTER_PAINTING_KEY = 'painter-painting-key'
export const GAN_PAINTING_KEY = 'gan-painting-key'
export const USE_THREE_PAINTING_KEY = 'use-three-painting'
export const buildSearchHomeKey = (search) => HIRE_ARTIST_SEARCH_KEY + "-" + search
export const buildArtistKey = (id) => 'artist'+ '-' + id
export const buildPaintingKey = (id) => 'painting'+ '-' + id
export const buildGanPaintingKey = (id) => 'gan-painting'+ '-' + id
export const MUTATION_KEYS =  {
    REGISTER_MEMBER: "registerMember",
    POST_MEMBER_IMAGE:'postMemberImage',
    REGISTER_ARTIST: "registerArtist",
    UPDATE_MEMBER: "updateMember",
    UPDATE_SPECIALIZATIONS: 'updateSpecializations',
    UPDATE_ARTIST: 'updateArtist',
    POST_SUBSCRIBE: 'postSubscribe',
    POST_BUY:'postBuy',
    POST_GAN:'postGAN',
    POST_IMAGE:'postImage',
    POST_PAINTING:'postPainting'
}
