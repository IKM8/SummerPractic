export default {
  extends: ['stylelint-config-standard-scss', 'stylelint-config-css-modules'],
  plugins: ['stylelint-order', 'stylelint-scss', 'stylelint-use-logical'],
  rules: {
    'order/properties-alphabetical-order': true,
    'csstools/use-logical': ['always', { except: ['width', 'height'] }]
  }
};
