import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {createTheme, CssBaseline} from "@mui/material";
import Unna from './assets/fonts/Unna-Regular.ttf';
import {ARTIST_BLOCK_GOLD, ARTIST_BLOCK_PURPLE} from "./utils/constants";
import {ThemeProvider} from "@emotion/react";
import {BrowserRouter as Router} from "react-router-dom";

const Theme = createTheme({
    palette: {
        primary: {
            main: ARTIST_BLOCK_PURPLE,
        },
        secondary: {
            main: ARTIST_BLOCK_GOLD
        },

    },
    typography: {
        fontFamily: 'Unna',
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: `
        @font-face {
          font-family: 'Unna';
          font-style: normal;
          font-display: swap;
          font-weight: 700;
          src: local('Unna'), local('Unna-Regular'), url(${Unna}) format('tff');
          unicodeRange: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF;
        }
      `,
        },
    },
});

ReactDOM.render(
    <Router>
        <ThemeProvider theme={Theme}>
            <CssBaseline />
            <App/>
        </ThemeProvider>
    </Router>,
    document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
