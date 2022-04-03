echo "Starting"

docker build -f staging.Dockerfile -t rolandsall24/artist-block-account-service:2.0.0 .

docker push rolandsall24/artist-block-account-service:2.0.0