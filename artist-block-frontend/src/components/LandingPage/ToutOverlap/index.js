import React from "react";
import { Parallax } from "react-scroll-parallax";
import Cta from "../Cta";

const ToutOverlap = props => (
    <div className={props.toutOverlapContent}>
        {/* <div className="toutImage toutImage--overlap" /> */}
        <Parallax className={props.toutOverlapCopy} y={[30, -30]} tagOuter="figure">
            <div className={props.totuOverlapText}>
                <h1 className="toutHeader--overlap">This is a Header</h1>
                <div className="toutText--overlap text-white">
                    <p>
                        Sed ut perspiciatis unde omnis iste natus error sit voluptatem
                        accusantium doloremque laudantium, totam rem aperiam, eaque ipsa
                        quae ab illo inventore veritatis et quasi architecto beatae vitae
                        dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit
                        aspernatur aut odit aut fugit, sed quia consequuntur magni dolores
                        eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est,
                        qui dolorem ipsum
                    </p>
                </div>
                <Cta />
            </div>
        </Parallax>
    </div>
);

export default ToutOverlap;
