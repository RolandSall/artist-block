import React, {useState, useEffect} from "react";
import fire from "./data/fire";
import {
    getAuth,
    onAuthStateChanged,
    createUserWithEmailAndPassword,
    signInWithEmailAndPassword,
    signOut
} from "firebase/auth"
import Login from "./layouts/login/Login";
import "swiper/swiper-bundle.min.css";
import "swiper/swiper.min.css";
import './App.css'
import {Route, Routes} from "react-router-dom";
import View1 from "./components/Stats";
import Navbar from "./components/Navbar/Navbar";


const App = () => {
    const auth = getAuth(fire);
    const [user, setUser] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    // handle any errors related to email and password
    const [emailError, setEmailError] = useState('');
    const [passwordError, setPasswordError] = useState('');
    const [hasAccount, setHasAccount] = useState(false);

    const clearInputs = () => {
        setEmail('');
        setPassword('');
    };

    const clearErrors = () => {
        setEmailError('');
        setPasswordError('');
    };

    const handleLogin = () => {
        clearErrors();
        signInWithEmailAndPassword(auth, email, password)
            .catch(err => {
                switch (err.code) {
                    case "auth/invalid-email":
                    case "auth/user-disabled":
                    case "auth/user-not-found":
                        setEmailError(err.message);
                        break;
                    case "auth/wrong-password":
                        setPasswordError(err.message);
                        break;
                }
            });
    };

    const handleSignup = () => {
        clearErrors();
        createUserWithEmailAndPassword(auth, email, password)
            .catch(err => {
                switch (err.code) {
                    case "auth/email-already-in-use":
                    case "auth/invalid-email":
                        setEmailError(err.message);
                        break;
                    case "auth/weak-password":
                        setPasswordError(err.message);
                        break;
                }
            });
    };

    const handleLogout = () => {
        signOut(auth);
    };

    const authListener = () => {
        onAuthStateChanged(auth, (user) => {
            if (user) {
                clearInputs();
                setUser(user);
            } else {
                setUser('');
            }
        });
    };

    useEffect(() => {
        authListener();
    }, []);

    console.log(user)

    return (
        <div className="App">
            {user ?  (

                <Routes>

                    <Route index element={<Navbar handleLogout={handleLogout} />} />
                    <Route path="/View1" element={<View1 />} />

                </Routes>
            ) : (
                <Login
                    email={email}
                    setEmail={setEmail}
                    password={password}
                    setPassword={setPassword}
                    handleLogin={handleLogin}
                    handleSignup={handleSignup}
                    hasAccount={hasAccount}
                    setHasAccount={setHasAccount}
                    emailError={emailError}
                    passwordError={passwordError}
                />
            )}
        </div>
    );
};

export default App;
