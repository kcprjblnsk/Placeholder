export const useFormValidationRules = () => {
	return {
		ruleRequired: (v) => !!v || "Wymagane",
		ruleEmail: (value) => {
			const pattern =
				/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
			return pattern.test(value) || "Niepoprawny adres email";
		},
		ruleUrl: (value) => {
			const pattern =
				/^(http:\/\/|https:\/\/).+$/;
			return pattern.test(value) || "Niepoprawny adres url";
		},
		ruleMaxLen: (max) => {
			return (v) => (!v || v.length <= max) || `Przekroczono maksymalną długość: ${max}`;
		},
	};
};