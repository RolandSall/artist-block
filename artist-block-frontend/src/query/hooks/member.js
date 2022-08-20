import {  useQuery } from 'react-query';
import * as Api from '../api';
import {
    buildArtistKey, buildGanPaintingKey,
    buildPaintingKey,
    CURRENT_ARTIST_KEY,
    CURRENT_MEMBER_KEY, GAN_PAINTING_KEY,
    OWN_PAINTING_KEY, PAINTER_PAINTING_KEY, PAINTING_PAINTER, USE_THREE_PAINTING, USE_THREE_PAINTING_KEY
} from "../config/keys";
import {useThreePainting} from "../api";

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

    const useOwnPainting = ({isAuthenticated,token}) => {
        return useQuery({
            queryKey: OWN_PAINTING_KEY,
            queryFn: async () =>
                Api.getOwnPaintings(await token, queryConfig).then((data) => data),
            enabled: Boolean(isAuthenticated) && Boolean(token),
            ...defaultOptions,
        });
    }

    const usePainterPainting = ({isAuthenticated,token, isArtist}) => {
        return useQuery({
            queryKey: PAINTER_PAINTING_KEY,
            queryFn: async () =>
                Api.getPainterPaintings(await token, queryConfig).then((data) => data),
            enabled: Boolean(isAuthenticated) && Boolean(token) && Boolean(isArtist),
            ...defaultOptions,
        });
    }

    const useGANPainting = ({isAuthenticated,token}) => {
        return useQuery({
            queryKey: GAN_PAINTING_KEY,
            queryFn: async () =>
                Api.getGanPaintings(await token, queryConfig).then((data) => data),
            enabled: Boolean(isAuthenticated) && Boolean(token),
            ...defaultOptions,
        });
    }

    // const useThreeRandomPainting = ({isAuthenticated,token}) => {
    //     return useQuery({
    //         queryKey: USE_THREE_PAINTING_KEY,
    //         queryFn: async () =>
    //             Api.useThreePainting(await token, queryConfig).then((data) => data),
    //         enabled: Boolean(isAuthenticated) && Boolean(token),
    //         ...defaultOptions,
    //     });
    // }

    const useCurrentMember = ({isAuthenticated,token}) => {
        return useQuery({
            queryKey: CURRENT_MEMBER_KEY,
            queryFn: async () =>
                Api.getCurrentMember(await token, queryConfig).then((data) => data),
            enabled: Boolean(isAuthenticated) && Boolean(token),
            ...defaultOptions,
        });
    }
    const useCurrentArtist= ({isArtist,token}) =>
        useQuery({
            queryKey: CURRENT_ARTIST_KEY,
            queryFn: async () =>
                Api.getCurrentArtist(await token,queryConfig).then((data) => data),
            enabled: isArtist,
            ...defaultOptions,
        });

    const useArtist = ({artistId, token}) =>
    useQuery({
        queryKey: buildArtistKey(artistId),
        queryFn: async () =>
            Api.getArtist(await token,artistId,queryConfig).then((data) => data),
        enabled: Boolean(artistId),
        ...defaultOptions,
    });

    const usePainting = ({paintingId}) => {
        console.log(paintingId)
        return useQuery({
            queryKey: buildPaintingKey(paintingId),
            queryFn: async () =>
                Api.getPainting(paintingId, queryConfig).then((data) => data),
            enabled: Boolean(paintingId),
            ...defaultOptions,
        });
    }

    const useGanPainting = ({paintingId}) => {
        console.log(paintingId)
        return useQuery({
            queryKey: buildGanPaintingKey(paintingId),
            queryFn: async () =>
                Api.getGanPainting(paintingId, queryConfig).then((data) => data),
            enabled: Boolean(paintingId),
            ...defaultOptions,
        });
    }
    const useGanImageProgress = ({enabled}) => {
        return useQuery({
            queryKey: 'GAN_PROGRESS_IMG',
            queryFn: async () =>
                Api.getGanImgProgress(queryConfig).then((data) => data),
            enabled,
            ...defaultOptions,
        });
    }
    const useGanImageProgressBar = ({enabled}) => {
        return useQuery({
            queryKey: 'GAN_PROGRESS_BAR',
            queryFn: async () =>
                Api.getGanProgressBar(queryConfig).then((data) => data),
            enabled,
            ...defaultOptions,
        });
    }

    const usePaintingPainter = ({painterId,token}) => {
        console.log("asdaskdasd" , painterId)

        return useQuery({
            queryKey: PAINTING_PAINTER,
            queryFn: async () =>
                Api.getPaintingPainter(await token, painterId, queryConfig).then((data) => data),
            ...defaultOptions,
        });
    }

    return {
        useCurrentMember,
        useCurrentArtist,
        useArtist,
        usePainting,
        usePaintingPainter,
        useGanPainting,
        useOwnPainting,
        useGANPainting,
        // useThreeRandomPainting,
        usePainterPainting,
        useGanImageProgress,
        useGanImageProgressBar
    }
}
