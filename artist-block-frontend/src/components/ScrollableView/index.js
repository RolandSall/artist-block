import React, { useRef, useState } from 'react';
import {
    ARTIST_GREEN,
    NAVBAR_HEIGHT, NAVBAR_NOMINAL_HEIGHT, ROUTES,
    SCROLL_BOUND,
    SCROLL_HANDLE_HEIGHT,
} from "../../utils/constants";
import Scroll, {scroller} from "react-scroll";
import useViewport from "../../hooks/useViewport";
import {useResizeObserver} from "beautiful-react-hooks";
import Draggable from "react-draggable";

import Logo from "../../assets/logos/logo.svg";
import './styles.scss';
import {useAuth0} from "@auth0/auth0-react";
import useAuth0Query from "../../hooks/useAuth0Query";
import {useLocation} from "react-router-dom";
import { matchPath } from "react-router";

let Element = Scroll.Element;

const ScrollableView = ({children}) => {

    const [position, setPosition] = useState({x: 0, y: SCROLL_BOUND})
    const { height: windowHeight } = useViewport()
    const scrollableContainer = useRef(null)
    const scrollSize = useResizeObserver(scrollableContainer)
    
    const { pathname } = useLocation()

    const route = ROUTES.find((e) => matchPath(pathname,{path:e.path,exact:true}))


    const { isAuthenticated } = useAuth0();
    const { token } = useAuth0Query();

    const changeHandlePosition = (y) => {
        setPosition({x:0,y})
    }

    const onDrag = (e, position) => {
        const {y} = position;
        changeHandlePosition(y)
        const absoluteY = y - SCROLL_BOUND
        const ratio = absoluteY / (windowHeight - 2 * SCROLL_BOUND  - NAVBAR_HEIGHT - SCROLL_HANDLE_HEIGHT)
        const offset = (scrollSize.height - windowHeight + NAVBAR_NOMINAL_HEIGHT) * ratio
        scroller.scrollTo("ScrollableContainer", {
            containerId: "scroll-id",
            hashSpy: true,
            spy: true,
            offset: offset,
        })
    }

    const containerScrollListener = (e) => {
        const scrollTop = e.target.scrollTop
        const ratio = scrollTop / (scrollSize.height - windowHeight + NAVBAR_NOMINAL_HEIGHT)
        const newAbsoluteY = ratio * (windowHeight - 2 * SCROLL_BOUND - NAVBAR_HEIGHT - SCROLL_HANDLE_HEIGHT)
        const newY = newAbsoluteY + SCROLL_BOUND
        changeHandlePosition(newY)
    }


    return (
        <div style={{width:'100%',display: 'flex', maxHeight: `calc( 100vh - ${NAVBAR_HEIGHT}px`,position: 'relative'}}>
            <div onScroll={containerScrollListener} id={"scroll-id"}
                 style={{flexGrow: 1, overflowY: 'scroll'}}>
                <Element name="ScrollableContainer">
                    <div ref={scrollableContainer}>
                       {children}
                    </div>
                </Element>
            </div>
            <div
                className={"draggable-dashboard-container"}
            >
                <Draggable
                    axis="y"
                    onDrag={onDrag}
                    position={{x: 0, y: position.y}}
                    bounds={{
                        top: SCROLL_BOUND,
                        bottom: windowHeight - NAVBAR_HEIGHT -  SCROLL_BOUND - SCROLL_HANDLE_HEIGHT}}
                    handle={'.scroll-handle'}
                >

                    <div style={{display: 'flex'}}>

                        <div
                            className="scroll-handle cursor-y"
                            style={{
                                background: `#FCFCFD url(${Logo}) no-repeat 5px center`,
                                // background: `${ARTIST_GREEN} no-repeat 5px center`,
                                height: `${SCROLL_HANDLE_HEIGHT}px`,
                            }}
                        >
                        </div>
                    </div>
                </Draggable>
            </div>
        </div>
    );
};

export default ScrollableView;
