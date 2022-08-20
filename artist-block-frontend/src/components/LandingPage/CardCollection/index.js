import React, { Fragment } from "react";
import Card from "../Card";

class CardCollection extends React.Component {
    render() {
        return (
            <Fragment>
                <div className="article-collection">
                    <Card cardMobile="w-11/12" cardCollection="mr-8 relative" />
                    <Card
                        cardMobile="w-11/12"
                        cardBackground="article-card__background-image"
                        cardCollection="mr-8 relative"
                    />
                    <Card cardMobile="w-11/12" cardCollection="mr-8 relative" />
                    <Card cardMobile="w-11/12" cardCollection="mr-8 relative" />
                    <Card
                        cardMobile="w-11/12"
                        cardBackground="article-card__background-image"
                        cardCollection="mr-8 relative"
                    />
                    <Card cardMobile="w-11/12" cardCollection="mr-8 relative" />
                </div>
            </Fragment>
        );
    }
}

export default CardCollection;
