import React from "react";
import ReactDom from "react-dom";
import App from "./App";
import {BrowserRouter} from "react-router-dom";


function Root(){
    return (

            <BrowserRouter>

            <App/>

            </BrowserRouter>

    );
}

ReactDom.render(<Root/>, document.getElementById('root'));
