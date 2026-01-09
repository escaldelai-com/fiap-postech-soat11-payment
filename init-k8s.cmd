@echo off

REM -- MongoDB
kubectl apply -f mongo-pay-service.yaml
kubectl apply -f mongo-pay-secrets.yaml
kubectl apply -f mongo-pay.yaml

REM -- App
kubectl apply -f app-pay-service.yaml
kubectl apply -f app-pay-ingress.yaml
kubectl apply -f app-pay.yaml

