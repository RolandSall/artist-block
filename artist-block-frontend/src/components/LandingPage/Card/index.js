import React from "react";
import classnames from "classnames";
import OnVisible, { setDefaultProps } from "react-on-visible";
import ArticleHeader from "../ArticleHeader";
import ViewArticle from "../ViewArticle";

const Card = props => (
    <OnVisible
        visibleClassName="card-appear"
        percent={50}
        className={classnames(
            "article-card p-8 pb-0",
            props.cardBackground,
            props.cardMobile,
            props.cardCollection
        )}
    >
        <ArticleHeader />
        <ViewArticle />
    </OnVisible>
);

export default Card;
