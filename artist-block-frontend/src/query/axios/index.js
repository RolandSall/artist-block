import axios from 'axios';
import {FALLBACK_TO_PUBLIC_FOR_STATUS_CODES} from '../config/constants';
import {UserIsSignedOut} from '../config/error';
import {isAuthenticated} from "./utils";

const configureAxios = () => {
    axios.defaults.withCredentials = true;
    return axios;
};

export function verifyAuthentication(request, returnValue) {
    if (!isAuthenticated) {
        if (returnValue) {
            return returnValue;
        }

        throw new UserIsSignedOut();
    }

    return request();
}

const returnFallbackDataOrThrow = (error, fallbackData) => {
    if (fallbackData) {
        return fallbackData;
    }

    throw error;
};

const fallbackForArray = async (
    data,
    publicRequest,
) => {
    // the array contains an error
    if (
        Array.isArray(data) &&
        data.some((d) =>
            FALLBACK_TO_PUBLIC_FOR_STATUS_CODES.includes(d?.statusCode),
        )
    ) {
        const publicCall = await publicRequest();
        const { data: publicData } = publicCall;

        // merge private and public valid data
        return data.map((d, idx) =>
            d?.statusCode ? publicData[idx] : d,
        );
    }
    return data;
};

/**
 * Automatically send request depending on whether member is authenticated
 * The function fallback to public depending on status code or authentication
 * @param request private axios request
 * @param publicRequest public axios request
 * @returns private request response, or public request response
 */
export const fallbackToPublic = (
    request,
    publicRequest,
    fallbackData,
) => {

    if (!isAuthenticated) {
        return publicRequest()
            .then(({ data }) => data)
            .catch((e) => returnFallbackDataOrThrow(e, fallbackData));
    }

    return request()
        .then(({ data }) => fallbackForArray(data, publicRequest))
        .catch((error) => {
            if (FALLBACK_TO_PUBLIC_FOR_STATUS_CODES.includes(error.response.status)) {
                return publicRequest()
                    .then(({ data }) => data)
                    .catch((e) => returnFallbackDataOrThrow(e, fallbackData));
            }

            return returnFallbackDataOrThrow(error, fallbackData);
        });
};

export default configureAxios;
