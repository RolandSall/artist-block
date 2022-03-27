import React, {useEffect} from "react";
import {Route, Switch} from "react-router-dom";
import {ROUTES} from "./utils/constants";
import './App.css'
import MainLayout from "./layouts/MainLayout";
import useAuth0Query from "./hooks/useAuth0Query";
import {useAuth0} from "@auth0/auth0-react";

const App = () => {

    const { token } = useAuth0Query()

    const { isAuthenticated} = useAuth0()

    useEffect(() => {

        const fetchToken = async () => {
            if (isAuthenticated) {
                console.log(await token())
            }
        }

        fetchToken()
    },[isAuthenticated])

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
