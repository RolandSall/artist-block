import Landing from "../../pages/Landing";
import RegistrationArtist from "../../pages/RegistrationArtist";
// import HomeIcon from '../../assets/core/drawer-icons/home.svg'
// import ArtistIcon from '../../assets/core/drawer-icons/artist.svg'
// import LLRIcon from '../../assets/core/drawer-icons/LLR.svg'
// import OverviewIcon from '../../assets/core/drawer-icons/overview.svg'
// import ServiceIcon from '../../assets/core/drawer-icons/service.svg'
import RegistrationCustomer from "../../pages/RegisterCustomer";
import GAN from "../../pages/GAN";
import HireAPainter from "../../pages/HireAPainter";
import PaintingProfile from "../../pages/PaintingProfile";
import UserProfile from "../../pages/UserProfile";
import PainterProfile from "../../pages/PainterProfile";
import GanProfile from "../../pages/GanProfile";
import PainterPaintingProfile from "../../components/PainterPaintingProfile";
// import Settings from "../../pages/Settings";
// import ArtistProfile from "../../pages/LaywerProfile";


export const HOME_ROUTE = {
    path: '/',
    name: 'Home',
    component: Landing,
    scrollable: true,
    footer: true,
    protectedRoute: false,
    exact: true
}

export const REGISTRATION_ARTIST_ROUTE = {
    path: '/register-artist',
    name: 'Register as a Artist',
    component: RegistrationArtist,
    scrollable: true,
    footer:true,
    protectedRoute:true,
    accountWarning: true,
    exact: true
}

export const REGISTRATION_CUSTOMER_ROUTE = {
    path: '/register',
    name: 'Register',
    component: RegistrationCustomer,
    scrollable: true,
    footer:true,
    protectedRoute:true,
    accountWarning: true,
    exact: true
}

export const GAN_ROUTE = {
    path: '/gan',
    name: 'Image Generator',
    component: GAN,
    scrollable: true,
    footer:true,
    protectedRoute:true,
    accountWarning: true,
    exact: true
}


export const HIRE_A_PAINTER_ROUTE = {
    path: '/hire-a-painter',
    name: 'Search',
    component:HireAPainter,
    scrollable: false,
    footer:false,
    protectedRoute: false,
    exact: true
}

export const PAINTING_PROFILE_ROUTE = {
    path: '/painting/:id',
    name: 'Paintings',
    component: PaintingProfile,
    // icon: OverviewIcon,
    scrollable: true,
    footer: true,
    exact: false
}


export const GAN_PROFILE_ROUTE = {
    path: '/gan-painting/:id',
    name: 'gan-paintings',
    component: GanProfile,
    // icon: OverviewIcon,
    scrollable: true,
    footer: true,
    exact: false
}




export const USER_PROFILE_ROUTE = {
    path: '/profile',
    name: 'Profile',
    component: UserProfile,
    // icon: OverviewIcon,
    protectedRoute: true,
    scrollable: true,
    footer: true,
    exact: false
}

export const PAINTER_PROFILE = {
    path: '/my-profile',
    name: 'My Profile',
    component: PainterProfile,
    // icon: OverviewIcon,
    scrollable: true,
    footer:true,
    protectedRoute:true,
    accountWarning: true,
    exact: true
}

export const PAINTER_PAINTING_PROFILE = {
    path: '/painter-profile/:id',
    name: 'Painter Profile',
    component: PainterPaintingProfile,
    // icon: OverviewIcon,
    scrollable: true,
    footer:true,
    protectedRoute:true,
    accountWarning: true,
    exact: true
}


export const ROUTES = [
    HOME_ROUTE,
    HIRE_A_PAINTER_ROUTE,
    REGISTRATION_ARTIST_ROUTE,
    REGISTRATION_CUSTOMER_ROUTE,
    GAN_ROUTE,
    PAINTING_PROFILE_ROUTE,
    USER_PROFILE_ROUTE,
    PAINTER_PROFILE,
    GAN_PROFILE_ROUTE,
    PAINTER_PAINTING_PROFILE
    // ARTIST_PROFILE_ROUTE
]

export const NAVBAR_ROUTES_MOBILE = [
    HOME_ROUTE,
    // HIRE_A_ARTIST_ROUTE,
    // LLR_ROUTE,
    // FOR_ARTISTS_ROUTE,
    // ABOUT_US_ROUTE,
    // FAQ_ROUTE
]

export const NAVBAR_ROUTES_DESKTOP = [
    HOME_ROUTE,
    REGISTRATION_CUSTOMER_ROUTE,
    HIRE_A_PAINTER_ROUTE,

 //   GAN_ROUTE,
    // HIRE_A_ARTIST_ROUTE,
    // LLR_ROUTE,
    [
        {
            name: 'More',
            paths: [

             GAN_ROUTE,
                PAINTER_PROFILE,
            ]
        }
    ],


]

export const FOOTER_ROUTES = [
    { pathname: '/careers', text: 'Careers' },
    { pathname: '/faq', text: 'FAQ' },
    { pathname: '/support', text: 'Support' },
    { pathname: '/contact-us', text: 'Contact Us' },
]

export const DASHBOARD_ROUTES = {}
