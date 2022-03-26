import React from "react";
import {Route, Switch} from "react-router-dom";
import {ROUTES} from "./utils/constants";
import './App.css'
import MainLayout from "./layouts/MainLayout";

const App = () => {




    return (
        <Switch>
            <MainLayout>
                {
                    ROUTES.map(({path,component,scrollable},index) => (
                        <Route key={`route-${index}`} path={path} exact component={component} />
                    ))
                }
            </MainLayout>
        </Switch>
    );
}

export default App;
