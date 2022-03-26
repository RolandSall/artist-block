//import Landing from "../../pages/Landing";
//import HomeIcon from '../../assets/core/drawer-icons/home.svg'



export const HOME_ROUTE = {
    path:'/',
    name:'Home',
    //component: Landing,
    //icon: HomeIcon,
    scrollable: true,
    footer:true
}

export const ROUTES = [
    HOME_ROUTE,
]

export const NAVBAR_ROUTES_MOBILE = [
    HOME_ROUTE,

]

export const FOOTER_ROUTES = [
    {pathname:'/careers',text:'Careers'},
    {pathname:'/faq',text:'FAQ'},
    {pathname:'/support',text:'Support'},
    {pathname:'/contact-us',text:'Contact Us'},
]

export const NAVBAR_ROUTES_DESKTOP = [
    HOME_ROUTE,
]

