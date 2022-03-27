// import Landing from "../../pages/Landing";
// import FAQ from "../../pages/FAQ";
// import HomeIcon HomeIconfrom '../../assets/core/drawer-icons/home.svg'

import Landing from "../../pages/Landing";

export const HOME_ROUTE = {
    path: '/',
    name: 'Home',
    component: Landing,
    // icon: HomeIcon,
    scrollable: true,
    footer: true,
    protectedRoute: false,
    exact: true
}


export const ROUTES = [
    HOME_ROUTE,

]

export const NAVBAR_ROUTES_MOBILE = [
    HOME_ROUTE,

]

export const NAVBAR_ROUTES_DESKTOP = [


]

export const FOOTER_ROUTES = [
    { pathname: '/careers', text: 'Careers' },
    { pathname: '/faq', text: 'FAQ' },
    { pathname: '/support', text: 'Support' },
    { pathname: '/contact-us', text: 'Contact Us' },
]

export const DASHBOARD_ROUTES = {}
