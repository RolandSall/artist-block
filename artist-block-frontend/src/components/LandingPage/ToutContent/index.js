import React from "react";
import ReactDOM from "react-dom";
import Fade from "react-reveal/Fade";
import ToutCopy from "../ToutCopy";
import ToutImage from "../ToutImage";

const ToutContent = props => (
    <div className={props.textDirection}>
        <ToutCopy />
        <ToutImage />
    </div>
);

export default ToutContent;
