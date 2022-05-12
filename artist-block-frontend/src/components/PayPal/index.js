import React, {useRef, useEffect, useState} from "react";
import {useParams} from "react-router-dom";
import {hooks, useMutation} from "../../query";
import {MUTATION_KEYS} from "../../query/config/keys";
import useAuth0Query from "../../hooks/useAuth0Query";
import {useAuth0} from "@auth0/auth0-react";

export default function Paypal() {
    const paypal = useRef();
    const { token } = useAuth0Query()
    const { getAccessTokenSilently} = useAuth0()
    const { id } = useParams();
    const { data: paintingData, isLoading} = hooks.usePainting({
        paintingId:id
    })
    const { mutate} = useMutation(MUTATION_KEYS.POST_BUY)

    useEffect(() => {
        window.paypal
            .Buttons({
                createOrder: (data, actions, err) => {
                    return actions.order.create({
                        intent: "CAPTURE",
                        purchase_units: [ // how to pass parameter to this
                            {
                                description: paintingData.paintingDescription,
                                amount: {
                                    currency_code: "USD",
                                    value: paintingData.paintingPrice,
                                },
                            },
                        ],
                    });
                },
                onApprove: async (data, actions) => {
                    const order = await actions.order.capture();
                    const t = await getAccessTokenSilently()
                    console.log(t)
                    mutate({
                        token:t,
                        paintingId:id,
                    })
                },
                onError: (err) => {
                    console.log(err);
                },
            })
            .render(paypal.current);
    }, []);

    return (
        <div>
            <div ref={paypal}></div>
        </div>
    );
}
