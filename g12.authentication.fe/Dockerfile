# production stage
FROM nginx:stable-alpine as production-stage
WORKDIR /app
COPY /dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]