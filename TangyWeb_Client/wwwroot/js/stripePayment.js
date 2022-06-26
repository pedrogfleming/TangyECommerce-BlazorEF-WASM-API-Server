redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51LCpuOE4YmMO4YuNcIxGhILl9ZvL1dS5427qrK8k8mRmQ4XbRS5YDa9ypIEzUR4eG76hbF2uTNS2RwwzdNFAYv2J00JFiyBK7U");
    stripe.redirectToCheckout({ sessionId: sessionId });
}
