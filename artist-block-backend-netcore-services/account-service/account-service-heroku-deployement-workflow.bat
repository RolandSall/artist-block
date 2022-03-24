echo "Starting"

docker build -f staging.Dockerfile -t rolandsall24/artist-block-account-service:1.0.5 .

docker push rolandsall24/artist-block-account-service:1.0.5