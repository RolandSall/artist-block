import {useQuery} from "react-query";
import {buildArtistKey, buildSearchHomeKey, HIRE_ARTIST_SEARCH_KEY, SEARCH_HOME_KEY} from "../config/keys";
import * as Api from "../api";

export default (
    queryClient,
    queryConfig,
) => {
    const {retry, cacheTime, staleTime} = queryConfig;
    const defaultOptions = {
        retry,
        cacheTime,
        staleTime,
    };

    const useHomeSearch = ({search, location}) =>
        useQuery({
            queryKey: buildSearchHomeKey(search),
            queryFn: async () =>
                Api.searchHomePage(search, location, queryConfig).then((data) => data),
            ...defaultOptions,
            enabled:false,
        });

    const useSearch = ({payload, token}) => {
        return useQuery({
            queryKey: HIRE_ARTIST_SEARCH_KEY,
            queryFn: async () =>
                Api.searchPage(payload, queryConfig, await token).then((data) => data),
            onSuccess: async (artists) => {
                if(artists.artists?.length>0) {
                //     // save items in their own key
                    artists.artists.forEach(async (artist) => {
                        const { artistId } = artist;

                        queryClient.setQueryData(
                            buildArtistKey(artistId),artist
                        );
                    });
                }
            },
            enabled: false,
            ...defaultOptions,
        });
    }
    return {
        useHomeSearch,
        useSearch
    }

}